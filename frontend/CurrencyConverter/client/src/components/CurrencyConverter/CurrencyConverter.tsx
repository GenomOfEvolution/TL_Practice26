import { useCallback, useState } from 'react';
import { useCurrencyConverter } from '../../hooks/useCurrencyConverter';
import { ConversionHeader } from '../ConversionHeader/ConversionHeader';
import { CurrencyInput } from '../CurrencyInput/CurrencyInput';
import { MoreAboutSection } from '../MoreAboutSection/MoreAboutSection';
import { SwapButton } from './SwapButton';
import { Toast } from '../Toast/Toast';
import styles from './CurrencyConverter.module.scss';

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
    swap
  } = useCurrencyConverter();

  const [dismissedError, setDismissedError] = useState<string | null>(null);

  const toastMessage = pricesError && pricesError !== dismissedError ? pricesError : null;

  const closeToast = useCallback(() => {
    if (pricesError) {
      setDismissedError(pricesError);
    }
  }, [pricesError]);

  if (currenciesLoading) {
    return (
      <div className={styles.exchanger}>
        <div className={styles.loadingState}>
          <span className={styles.loader} data-testid="currency-converter-main-loader" />
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
      {toastMessage && <Toast message={toastMessage} onClose={closeToast} />}
      <ConversionHeader from={from} to={to} amount={amount} result={result} dateTime={dateTime} />
      <CurrencyInput
        value={amount}
        currency={from}
        currencies={currencies}
        onAmountChange={setAmount}
        onCurrencyChange={setFrom}
      />
      <div className={styles.swapWrapper}>
        <SwapButton onClick={swap} />
      </div>
      <CurrencyInput
        value={result}
        currency={to}
        currencies={currencies}
        onAmountChange={setAmount}
        onCurrencyChange={setTo}
        readOnly
      />
      <MoreAboutSection key={`${from.code}-${to.code}`} from={from} to={to} />
    </div>
  );
};
