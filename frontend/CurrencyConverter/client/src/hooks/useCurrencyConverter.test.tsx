import { describe, expect, it, vi, beforeEach } from 'vitest';
import { useCurrencyConverter } from './useCurrencyConverter';
import { act, renderHook, waitFor } from '@testing-library/react';
import type { CurrencyDto, PriceChangeDto } from '../models/dto';
import type { Currency } from '../models';
import { mapCurrency } from '../models/mappers';
import currenciesJson from '../mocks/2_hw_mock_currencies.json';
import priceChangesJson from '../mocks/2_hw_mock_price_changes.json';

const { mockedFetchCurrencies, mockedFetchPriceChanges } = vi.hoisted(() => ({
  mockedFetchCurrencies: vi.fn(),
  mockedFetchPriceChanges: vi.fn(),
}));

vi.mock('../api/currencyApi', () => ({
  fetchCurrencies: mockedFetchCurrencies,
  fetchPriceChanges: mockedFetchPriceChanges,
}));

const mockCurrenciesDto = currenciesJson as CurrencyDto[];
const mockPriceChangesMap = priceChangesJson as Record<string, Record<string, PriceChangeDto>>;

const getCurrency = (code: string): Currency => {
  const dto = mockCurrenciesDto.find((c) => c.code === code);
  if (!dto) throw new Error(`Currency ${code} not found`);
  return mapCurrency(dto);
};

const getPriceChangesArray = (paymentCurrency: string, purchasedCurrency: string): PriceChangeDto[] => {
  const entry = mockPriceChangesMap[paymentCurrency]?.[purchasedCurrency];
  return entry ? [entry] : [];
};

beforeEach(() => {
  mockedFetchCurrencies.mockResolvedValue(mockCurrenciesDto);
  mockedFetchPriceChanges.mockImplementation((paymentCurrency, purchasedCurrency) =>
    Promise.resolve(getPriceChangesArray(paymentCurrency, purchasedCurrency)),
  );
});

describe('useCurrencyConverter', () => {
  it('auto-replaces to when setFrom matches current to', async () => {
    const { result } = renderHook(() => useCurrencyConverter());

    await waitFor(() => expect(result.current.from).not.toBeNull());

    act(() => {
      result.current.setTo(getCurrency('PLN'));
    });

    await waitFor(() => {
      expect(result.current.to?.code).toBe('PLN');
    });

    act(() => {
      result.current.setFrom(getCurrency('PLN'));
    });

    await waitFor(() => {
      expect(result.current.from?.code).toBe('PLN');
      expect(result.current.to?.code).not.toBe('PLN');
    });
  });

  it('swap correctly recalculates result', async () => {
    const { result } = renderHook(() => useCurrencyConverter());

    await waitFor(() => expect(result.current.from).not.toBeNull());

    act(() => {
      result.current.setFrom(getCurrency('PLN'));
      result.current.setTo(getCurrency('CAD'));
      result.current.setAmount('1');
    });

    await waitFor(() => expect(result.current.result).not.toBe(''));

    const oldResult = Number(result.current.result);

    act(() => {
      result.current.swap();
    });

    await waitFor(() => {
      const newResult = Number(result.current.result);
      expect(oldResult * newResult).toBeCloseTo(1, 1);
    });
  });
});
