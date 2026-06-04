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
        inputTestId="from-amount"
        selectTestId="from-currency"
      />
      <div className={styles.swapWrapper}>
        <SwapButton onClick={swap} testId="swap-button" />
      </div>
      <CurrencyInput
        value={result}
        currency={to}
        currencies={currencies}
        onAmountChange={setAmount}
        onCurrencyChange={setTo}
        readOnly
        inputTestId="to-amount"
        selectTestId="to-currency"
      />
      {/*
        key={`${from}-${to}`} — при смене валютной пары React удаляет старый
        MoreAboutSection и создаёт новый, сбрасывая isOpen в false.
        Без key пришлось бы использовать useEffect + setIsOpen(false),
        что не гарантирует сброс всех видов локального состояния (ref и т.д.).
      */}
      <MoreAboutSection key={`${from}-${to}`} from={from} to={to} currencies={currencies} />
    </div>
  );
};
