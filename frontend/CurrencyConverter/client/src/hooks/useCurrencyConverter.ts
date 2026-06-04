import { useState, useMemo, useCallback } from 'react';
import { currencies, priceChanges } from '../mocks';

const findFirstDifferent = (code: string): string => {
  const found = currencies.find((c) => c.code !== code);

  return found ? found.code : code;
};

export const useCurrencyConverter = () => {
  const [from, setFromState] = useState(currencies[0].code);
  const [to, setToState] = useState(currencies[1].code);
  const [amount, setAmount] = useState('1');

  const setFrom = useCallback((code: string) => {
    setFromState(code);
    setToState((prevTo) => (code === prevTo ? findFirstDifferent(code) : prevTo));
  }, []);

  const setTo = useCallback((code: string) => {
    setToState(code);
    setFromState((prevFrom) => (code === prevFrom ? findFirstDifferent(code) : prevFrom));
  }, []);

  const swap = useCallback(() => {
    setFromState(to);
    setToState(from);
  }, [from, to]);

  const result = useMemo(() => {
    const rate = priceChanges[from]?.[to]?.price;

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
