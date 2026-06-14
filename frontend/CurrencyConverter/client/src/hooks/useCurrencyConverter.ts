import { useState, useEffect, useReducer } from 'react';
import type { Currency, PriceChange } from '../models';
import { dataReducer, createInitialState } from './dataReducer';
import { fetchCurrencies, fetchPriceChanges } from '../api/currencyApi';
import { mapCurrency, mapPriceChange } from '../models/mappers';

const findFirstDifferent = (list: Currency[], code: string): Currency => {
  const found = list.find((c) => c.code !== code);

  return found ? found : list[0];
};

export const useCurrencyConverter = () => {
  const [currenciesState, dispatchCurrencies] = useReducer(dataReducer<Currency[]>, createInitialState<Currency[]>());

  const [pricesState, dispatchPrices] = useReducer(dataReducer<PriceChange[]>, createInitialState<PriceChange[]>());

  const [from, setFromState] = useState<Currency | undefined>(undefined);
  const [to, setToState] = useState<Currency | undefined>(undefined);
  const [amount, setAmount] = useState('1');

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

  useEffect(() => {
    if (!from || !to) return;

    const abortController = new AbortController();

    const load = async () => {
      dispatchPrices({ type: 'LOADING' });
      try {
        const response = await fetchPriceChanges(
          from.code,
          to.code,
          new Date(0).toISOString(),
          undefined,
          abortController.signal
        );
        const mapped = response.map(mapPriceChange);
        dispatchPrices({ type: 'SUCCESS', payload: mapped });
      } catch (err) {
        if (err instanceof Error && err.name === 'AbortError') return;

        dispatchPrices({
          type: 'ERROR',
          payload: err instanceof Error ? err.message : 'Unknown error'
        });
      }
    };

    load();

    return () => {
      abortController.abort();
    };
  }, [from, to]);

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
    pricesError: pricesState.error,
    setFrom,
    setTo,
    setAmount,
    swap
  };
};
