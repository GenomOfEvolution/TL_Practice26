import { useCurrencyConverter } from '../../hooks/useCurrencyConverter';
import { ConversionHeader } from '../ConversionHeader/ConversionHeader';
import { CurrencyInput } from '../CurrencyInput/CurrencyInput';
import { MoreAboutSection } from '../MoreAboutSection/MoreAboutSection';
import { SwapButton } from './SwapButton';
import styles from './CurrencyConverter.module.scss';

export const CurrencyConverter = () => {
  const { from, to, amount, result, currencies, priceChanges, setFrom, setTo, setAmount, swap } =
    useCurrencyConverter();

  const fromCurrency = currencies.find((c) => c.code === from);
  const toCurrency = currencies.find((c) => c.code === to);
  const dateTime = priceChanges[from]?.[to]?.dateTime;

  return (
    <div className={styles.exchanger}>
      {fromCurrency && toCurrency && (
        <ConversionHeader from={fromCurrency} to={toCurrency} amount={amount} result={result} dateTime={dateTime} />
      )}
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
      <MoreAboutSection from={from} to={to} currencies={currencies} />
    </div>
  );
};
