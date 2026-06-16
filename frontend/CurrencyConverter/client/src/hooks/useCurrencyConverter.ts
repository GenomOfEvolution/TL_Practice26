import { useState, useEffect, useReducer } from 'react';
import type { Currency } from '../models';
import { dataReducer, createInitialState } from './dataReducer';
import { fetchCurrencies } from '../api/currencyApi';
import { mapCurrency } from '../models/mappers';
import { usePriceHistory } from './usePriceHistory';

const DEFAULT_PERIOD = 5;

const findFirstDifferent = (list: Currency[], code: string): Currency => {
  const found = list.find((c) => c.code !== code);

  return found ? found : list[0];
};

export const useCurrencyConverter = () => {
  const [currenciesState, dispatchCurrencies] = useReducer(dataReducer<Currency[]>, createInitialState<Currency[]>());

  const [from, setFromState] = useState<Currency | undefined>(undefined);
  const [to, setToState] = useState<Currency | undefined>(undefined);
  const [amount, setAmount] = useState('1');
  const [period, setPeriod] = useState(DEFAULT_PERIOD);
  const pricesState = usePriceHistory(from, to, period);

  useEffect(() => {
    const abortController = new AbortController();

    const load = async () => {
      dispatchCurrencies({ type: 'LOADING' });
      try {
        const response = await fetchCurrencies(abortController.signal);
        const mapped = response.map(mapCurrency);

        dispatchCurrencies({ type: 'SUCCESS', payload: mapped });
        setFromState(mapped[0]);
        setToState(mapped[1]);
      } catch (err) {
        if (err instanceof DOMException && err.name === 'AbortError') return;

        dispatchCurrencies({
          type: 'ERROR',
          payload: err instanceof Error ? err.message : 'Unknown error'
        });
      }
    };

    load();

    return () => {
      abortController.abort();
    };
  }, []);

  const currencies = currenciesState.data ?? [];
  const priceChanges = pricesState.data ?? [];

  const setFrom = (currency: Currency) => {
    if (!to) return;
    setFromState(currency);
    if (currency.code === to.code) {
      setToState(findFirstDifferent(currencies, currency.code));
    }
  };

  const setTo = (currency: Currency) => {
    if (!from) return;
    setToState(currency);
    if (currency.code === from.code) {
      setFromState(findFirstDifferent(currencies, currency.code));
    }
  };

  const swap = () => {
    if (!from || !to) return;
    setFromState(to);
    setToState(from);
  };

  const latestPriceChange = priceChanges[priceChanges.length - 1];

  const result = (() => {
    if (!latestPriceChange) return '';

    const numAmount = Number(amount);

    if (Number.isNaN(numAmount) || amount === '') return '';

    return String(numAmount * latestPriceChange.price);
  })();

  return {
    from,
    to,
    amount,
    result,
    currencies,
    priceChanges,
    currenciesLoading: currenciesState.loading,
    currenciesError: currenciesState.error,
    pricesLoading: pricesState.loading,
    pricesError: pricesState.error,
    period,
    setFrom,
    setTo,
    setAmount,
    setPeriod,
    swap
  };
};
