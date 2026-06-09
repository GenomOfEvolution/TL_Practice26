import type { CurrencyDto, PriceChangeDto } from '../models/dto';

const BASE_URL = 'http://localhost:5081';

export const fetchCurrencies = async (): Promise<CurrencyDto[]> => {
  const response = await fetch(`${BASE_URL}/Currency`);

  if (!response.ok) {
    throw new Error(`Failed to fetch currencies: ${response.status}`);
  }

  return response.json();
};

export const fetchPriceChanges = async (
  paymentCurrency: string,
  purchasedCurrency: string,
  fromDateTime: string,
  toDateTime?: string,
): Promise<PriceChangeDto[]> => {
  const params = new URLSearchParams({
    paymentCurrency,
    purchasedCurrency,
    fromDateTime,
  });

  if (toDateTime) {
    params.set('toDateTime', toDateTime);
  }

  const response = await fetch(`${BASE_URL}/prices?${params}`);

  if (!response.ok) {
    throw new Error(`Failed to fetch prices: ${response.status}`);
  }

  return response.json();
};
