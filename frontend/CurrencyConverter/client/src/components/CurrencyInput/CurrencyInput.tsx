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
  inputTestId?: string;
  selectTestId?: string;
};

export const CurrencyInput = ({
  value,
  currency,
  currencies,
  onAmountChange,
  onCurrencyChange,
  readOnly = false,
  inputTestId,
  selectTestId
}: CurrencyInputProps) => {
  const handleAmountChange = (e: ChangeEvent<HTMLInputElement>) => {
    onAmountChange(e.target.value);
  };

  const handleCurrencyChange = (e: ChangeEvent<HTMLSelectElement>) => {
    onCurrencyChange(e.target.value);
  };

  return (
    <div className={styles.container}>
      <input className={styles.input} type="text" value={value} onChange={handleAmountChange} readOnly={readOnly} data-testid={inputTestId} />
      <div className={styles.separator} />
      <div className={styles.dropdown}>
        <select className={styles.select} value={currency} onChange={handleCurrencyChange} data-testid={selectTestId}>
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
