import { useReducer, useEffect, useCallback } from 'react';
import { useCurrencyConverter } from '../../hooks/useCurrencyConverter';
import { ConversionHeader } from '../ConversionHeader/ConversionHeader';
import { CurrencyInput } from '../CurrencyInput/CurrencyInput';
import { MoreAboutSection } from '../MoreAboutSection/MoreAboutSection';
import { SwapButton } from './SwapButton';
import { Toast } from '../Toast/Toast';
import styles from './CurrencyConverter.module.scss';

type ToastState = { message: string | null };

type ToastAction = { type: 'SHOW'; message: string } | { type: 'HIDE' };

const toastReducer = (_state: ToastState, action: ToastAction): ToastState => {
  switch (action.type) {
    case 'SHOW':
      return { message: action.message };
    case 'HIDE':
      return { message: null };
    default:
      return { message: null };
  }
};

export const CurrencyConverter = () => {
  const {
    from,
    to,
    amount,
    result,
    currencies,
    priceChanges,
    currenciesLoading,
    currenciesError,
    pricesError,
    setFrom,
    setTo,
    setAmount,
    swap,
  } = useCurrencyConverter();

  const [toastState, dispatchToast] = useReducer(toastReducer, { message: null });

  useEffect(() => {
    if (pricesError) {
      dispatchToast({ type: 'SHOW', message: pricesError });
    }
  }, [pricesError]);

  const closeToast = useCallback(() => dispatchToast({ type: 'HIDE' }), []);

  if (currenciesLoading) {
    return (
      <div className={styles.exchanger}>
        <div className={styles.loadingState}>
          <span className={styles.loader} />
        </div>
      </div>
    );
  }

  if (currenciesError) {
    return (
      <div className={styles.exchanger}>
        <div className={styles.errorBlock}>
          <h2>Server is not available</h2>
          <p>{currenciesError}</p>
          <p>Please try again later.</p>
        </div>
      </div>
    );
  }

  if (!from || !to) {
    return (
      <div className={styles.exchanger}>
        <p>Loading...</p>
      </div>
    );
  }

  const latestPriceChange = priceChanges[priceChanges.length - 1];
  const dateTime = latestPriceChange?.dateTime;

  return (
    <div className={styles.exchanger}>
      {toastState.message && <Toast message={toastState.message} onClose={closeToast} />}
      <ConversionHeader from={from} to={to} amount={amount} result={result} dateTime={dateTime} />
      <CurrencyInput
        value={amount}
        currency={from.code}
        currencies={currencies}
        onAmountChange={setAmount}
        onCurrencyChange={setFrom}
      />
      <div className={styles.swapWrapper}>
        <SwapButton onClick={swap} />
      </div>
      <CurrencyInput
        value={result}
        currency={to.code}
        currencies={currencies}
        onAmountChange={setAmount}
        onCurrencyChange={setTo}
        readOnly
      />
      <MoreAboutSection key={`${from.code}-${to.code}`} from={from} to={to} />
    </div>
  );
};
