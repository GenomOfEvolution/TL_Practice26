import type { CurrencyDto, PriceChangeDto } from './dto';
import type { Currency, PriceChange } from './index';

export const mapCurrency = (dto: CurrencyDto): Currency => ({
  code: dto.code,
  name: dto.name,
  description: dto.description,
  symbol: dto.symbol,
});

export const mapPriceChange = (dto: PriceChangeDto): PriceChange => ({
  purchasedCurrencyCode: dto.purchasedCurrencyCode,
  paymentCurrencyCode: dto.paymentCurrencyCode,
  price: dto.price,
  dateTime: dto.dateTime,
});
