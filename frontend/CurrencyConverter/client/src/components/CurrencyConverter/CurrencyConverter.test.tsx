import { render, screen, waitFor, act } from '@testing-library/react';
import { describe, expect, it, vi, beforeEach } from 'vitest';
import { CurrencyConverter } from './CurrencyConverter';
import type { CurrencyDto, PriceChangeDto } from '../../models/dto';
import currenciesJson from '../../mocks/2_hw_mock_currencies.json';
import priceChangesJson from '../../mocks/2_hw_mock_price_changes.json';

vi.mock('../../api/currencyApi', () => ({
  fetchCurrencies: vi.fn(),
  fetchPriceChanges: vi.fn()
}));

import { fetchCurrencies, fetchPriceChanges } from '../../api/currencyApi';

const mockedFetchCurrencies = vi.mocked(fetchCurrencies);
const mockedFetchPriceChanges = vi.mocked(fetchPriceChanges);

const mockCurrenciesDto = currenciesJson as CurrencyDto[];
const mockPriceChangesMap = priceChangesJson as Record<string, Record<string, PriceChangeDto>>;

const getPriceChangesArray = (paymentCurrency: string, purchasedCurrency: string): PriceChangeDto[] => {
  const entry = mockPriceChangesMap[paymentCurrency]?.[purchasedCurrency];
  return entry ? [entry] : [];
};

beforeEach(() => {
  mockedFetchCurrencies.mockResolvedValue(mockCurrenciesDto);
  mockedFetchPriceChanges.mockImplementation((paymentCurrency, purchasedCurrency) =>
    Promise.resolve(getPriceChangesArray(paymentCurrency, purchasedCurrency))
  );
});

describe('CurrencyConverter', () => {
  it('shows spinner while loading and renders converter after load', async () => {
    let resolvePromise: (value: CurrencyDto[]) => void;
    mockedFetchCurrencies.mockImplementation(
      () =>
        new Promise((resolve) => {
          resolvePromise = resolve;
        })
    );

    render(<CurrencyConverter />);

    expect(screen.getByTestId('currency-converter-main-loader')).toBeInTheDocument();

    await act(async () => {
      resolvePromise!(mockCurrenciesDto);
    });

    await waitFor(() => {
      expect(screen.getByDisplayValue('1')).toBeInTheDocument();
    });
  });

  it('shows server error block when currencies fail to load', async () => {
    mockedFetchCurrencies.mockRejectedValue(new Error('Network error'));

    render(<CurrencyConverter />);

    await waitFor(() => {
      expect(screen.getByText('Server is not available')).toBeInTheDocument();
    });
  });

  it('renders inputs, selects, swap button and more-about button after successful load', async () => {
    render(<CurrencyConverter />);

    await waitFor(() => {
      expect(screen.getByDisplayValue('1')).toBeInTheDocument();
    });

    expect(screen.getByDisplayValue('CAD')).toBeInTheDocument();
    expect(screen.getByDisplayValue('PLN')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: /swap/i })).toBeInTheDocument();
    expect(screen.getByText(/CAD\/PLN.*about/)).toBeInTheDocument();
  });

  it('shows toast when prices error occurs', async () => {
    mockedFetchPriceChanges.mockRejectedValue(new Error('Prices fetch failed'));

    render(<CurrencyConverter />);

    await waitFor(() => {
      expect(screen.getByText('Prices fetch failed')).toBeInTheDocument();
    });
  });
});
