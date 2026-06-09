import { useCurrencyConverter } from '../../hooks/useCurrencyConverter';
import { ConversionHeader } from '../ConversionHeader/ConversionHeader';
import { CurrencyInput } from '../CurrencyInput/CurrencyInput';
import { MoreAboutSection } from '../MoreAboutSection/MoreAboutSection';
import { SwapButton } from './SwapButton';
import styles from './CurrencyConverter.module.scss';

export const CurrencyConverter = () => {
  const { from, to, amount, result, currencies, priceChanges, setFrom, setTo, setAmount, swap } =
    useCurrencyConverter();

  const dateTime = priceChanges[from.code]?.[to.code]?.dateTime;

  return (
    <div className={styles.exchanger}>
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
