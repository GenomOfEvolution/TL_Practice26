import type { Currency, PriceChange } from '../models';
import currenciesJson from './2_hw_mock_currencies.json';
import priceChangesJson from './2_hw_mock_price_changes.json';

export const currencies: Currency[] = currenciesJson;

export const priceChanges: Record<string, Record<string, PriceChange>> = priceChangesJson;
