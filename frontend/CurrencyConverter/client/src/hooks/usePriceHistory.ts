import { useEffect, useReducer } from 'react';
import type { Currency, PriceChange } from '../models';
import { dataReducer, createInitialState } from './dataReducer';
import { fetchPriceChanges } from '../api/currencyApi';
import { mapPriceChange } from '../models/mappers';

const MS_IN_MINUTE = 60_000;
const POLL_INTERVAL_MS = 10_000;

export const usePriceHistory = (from?: Currency, to?: Currency, period: number = 5) => {
  const [state, dispatch] = useReducer(dataReducer<PriceChange[]>, createInitialState());

  const fromCode = from?.code;
  const toCode = to?.code;

  useEffect(() => {
    if (!fromCode || !toCode) return;

    const abortController = new AbortController();

    const fetchData = () => {
      const now = new Date();
      const fromDateTime = new Date(now.getTime() - period * MS_IN_MINUTE).toISOString();
      const toDateTime = now.toISOString();

      fetchPriceChanges(fromCode, toCode, fromDateTime, toDateTime, abortController.signal)
        .then((dto) => dto.map(mapPriceChange))
        .then((mapped) => dispatch({ type: 'SUCCESS', payload: mapped }))
        .catch((err) => {
          if (err instanceof DOMException && err.name === 'AbortError') return;

          dispatch({ type: 'ERROR', payload: err instanceof Error ? err.message : 'Unknown error' });
        });
    };

    dispatch({ type: 'LOADING' });
    fetchData();

    const intervalId = setInterval(fetchData, POLL_INTERVAL_MS);

    return () => {
      abortController.abort();
      clearInterval(intervalId);
    };
  }, [fromCode, toCode, period]);

  return state;
};
