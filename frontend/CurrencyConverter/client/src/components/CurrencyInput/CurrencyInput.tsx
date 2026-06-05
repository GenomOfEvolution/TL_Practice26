import type { ChangeEvent } from 'react';
import type { Currency } from '../../models';
import styles from './CurrencyInput.module.scss';
import { CurrencyListButton } from './CurrencyListButton';

type CurrencyInputProps = {
  value: string;
  currency: string;
  currencies: Currency[];
  onAmountChange: (value: string) => void;
  onCurrencyChange: (code: string) => void;
  readOnly?: boolean;
};

export const CurrencyInput = ({
  value,
  currency,
  currencies,
  onAmountChange,
  onCurrencyChange,
  readOnly = false,
}: CurrencyInputProps) => {
  const handleAmountChange = (e: ChangeEvent<HTMLInputElement>) => {
    onAmountChange(e.target.value);
  };

  const handleCurrencyChange = (e: ChangeEvent<HTMLSelectElement>) => {
    onCurrencyChange(e.target.value);
  };

  return (
    <div className={styles.container}>
      <input className={styles.input} type="text" value={value} onChange={handleAmountChange} readOnly={readOnly} />
      <div className={styles.separator} />
      <div className={styles.dropdown}>
        <select className={styles.select} value={currency} onChange={handleCurrencyChange}>
          {currencies.map((c) => (
            <option key={c.code} value={c.code}>
              {c.code}
            </option>
          ))}
        </select>
        <div className={styles.buttonOverlay}>
          <CurrencyListButton />
        </div>
      </div>
    </div>
  );
};
