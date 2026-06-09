export type Currency = {
  code: string;
  name: string;
  description: string;
  symbol: string;
};

export type PriceChange = {
  purchasedCurrencyCode: string;
  paymentCurrencyCode: string;
  price: number;
  dateTime: string;
};
