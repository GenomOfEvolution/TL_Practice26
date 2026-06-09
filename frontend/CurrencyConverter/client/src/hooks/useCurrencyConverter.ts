import { useState, useMemo, useCallback, useEffect, useReducer } from 'react';
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

  const [from, setFromState] = useState<Currency | null>(null);
  const [to, setToState] = useState<Currency | null>(null);
  const [amount, setAmount] = useState('1');

  useEffect(() => {
    let cancelled = false;

    const load = async () => {
      dispatchCurrencies({ type: 'LOADING' });
      try {
        const dtos = await fetchCurrencies();
        const mapped = dtos.map(mapCurrency);

        if (cancelled) return;

        dispatchCurrencies({ type: 'SUCCESS', payload: mapped });
        setFromState(mapped[0]);
        setToState(mapped[1] ?? mapped[0]);
      } catch (err) {
        if (cancelled) return;

        dispatchCurrencies({
          type: 'ERROR',
          payload: err instanceof Error ? err.message : 'Unknown error'
        });
      }
    };

    load();

    return () => {
      cancelled = true;
    };
  }, []);

  useEffect(() => {
    if (!from || !to) return;

    const load = async () => {
      dispatchPrices({ type: 'LOADING' });
      try {
        const dtos = await fetchPriceChanges(from.code, to.code, new Date(0).toISOString());
        const mapped = dtos.map(mapPriceChange);
        dispatchPrices({ type: 'SUCCESS', payload: mapped });
      } catch (err) {
        dispatchPrices({
          type: 'ERROR',
          payload: err instanceof Error ? err.message : 'Unknown error'
        });
      }
    };

    load();
  }, [from, to]);

  const currencies = useMemo(() => currenciesState.data ?? [], [currenciesState.data]);
  const priceChanges = useMemo(() => pricesState.data ?? [], [pricesState.data]);

  const setFrom = useCallback(
    (code: string) => {
      const currency = currencies.find((c) => c.code === code);
      if (!currency || !to) return;
      setFromState(currency);
      if (code === to.code) {
        setToState(findFirstDifferent(currencies, code));
      }
    },
    [currencies, to]
  );

  const setTo = useCallback(
    (code: string) => {
      const currency = currencies.find((c) => c.code === code);
      if (!currency || !from) return;
      setToState(currency);
      if (code === from.code) {
        setFromState(findFirstDifferent(currencies, code));
      }
    },
    [currencies, from]
  );

  const swap = useCallback(() => {
    if (!from || !to) return;
    setFromState(to);
    setToState(from);
  }, [from, to]);

  const latestPriceChange = priceChanges[priceChanges.length - 1];

  const result = useMemo(() => {
    if (!latestPriceChange) return '';

    const numAmount = Number(amount);

    if (Number.isNaN(numAmount) || amount === '') return '';

    return String(numAmount * latestPriceChange.price);
  }, [latestPriceChange, amount]);

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
