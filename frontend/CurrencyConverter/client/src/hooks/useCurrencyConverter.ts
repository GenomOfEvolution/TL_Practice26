import { useState, useMemo, useCallback } from 'react';
import type { Currency } from '../models';
import { currencies, priceChanges } from '../mocks';

const findFirstDifferent = (code: string): Currency => {
  const found = currencies.find((c) => c.code !== code);

  return found ? found : currencies[0];
};

export const useCurrencyConverter = () => {
  const [from, setFromState] = useState<Currency>(currencies[0]);
  const [to, setToState] = useState<Currency>(currencies[1]);
  const [amount, setAmount] = useState('1');

  const setFrom = useCallback((code: string) => {
    const currency = currencies.find((c) => c.code === code);
    if (!currency) return;
    setFromState(currency);
    setToState((prevTo) => (code === prevTo.code ? findFirstDifferent(code) : prevTo));
  }, []);

  const setTo = useCallback((code: string) => {
    const currency = currencies.find((c) => c.code === code);
    if (!currency) return;
    setToState(currency);
    setFromState((prevFrom) => (code === prevFrom.code ? findFirstDifferent(code) : prevFrom));
  }, []);

  const swap = useCallback(() => {
    setFromState(to);
    setToState(from);
  }, [from, to]);

  const result = useMemo(() => {
    const rate = priceChanges[from.code]?.[to.code]?.price;

    if (rate === null) {
      return '';
    }

    const numAmount = Number(amount);

    if (Number.isNaN(numAmount) || amount === '') {
      return '';
    }

    return String(numAmount * rate);
  }, [from, to, amount]);

  return {
    from,
    to,
    amount,
    result,
    currencies,
    priceChanges,
    setFrom,
    setTo,
    setAmount,
    swap
  };
};
