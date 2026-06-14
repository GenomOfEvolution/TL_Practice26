import { describe, expect, it } from 'vitest';
import type { CurrencyDto, PriceChangeDto } from './dto';
import { mapCurrency, mapPriceChange } from './mappers';

describe('mapCurrency', () => {
  it('maps CurrencyDto to Currency', () => {
    const dto: CurrencyDto = {
      code: 'USD',
      name: 'United States dollar',
      description: 'The US dollar is the official currency of the United States.',
      symbol: '$',
    };

    const result = mapCurrency(dto);

    expect(result).toEqual({
      code: 'USD',
      name: 'United States dollar',
      description: 'The US dollar is the official currency of the United States.',
      symbol: '$',
    });
  });
});

describe('mapPriceChange', () => {
  it('maps PriceChangeDto to PriceChange', () => {
    const dto: PriceChangeDto = {
      purchasedCurrencyCode: 'JPY',
      paymentCurrencyCode: 'CAD',
      price: 0.741,
      dateTime: '2026-05-21T03:40:54.2709677Z',
    };

    const result = mapPriceChange(dto);

    expect(result).toEqual({
      purchasedCurrencyCode: 'JPY',
      paymentCurrencyCode: 'CAD',
      price: 0.741,
      dateTime: '2026-05-21T03:40:54.2709677Z',
    });
  });
});
