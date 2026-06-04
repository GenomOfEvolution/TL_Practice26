import styles from './CurrencyInput.module.scss';
import { CurrencyListButton } from './CurrencyListButton';

type CurrencyInputProps = {
  value: string;
  currency: string;
};

export const CurrencyInput = ({ value, currency }: CurrencyInputProps) => {
  return (
    <div className={styles.container}>
      <input className={styles.input} type="text" value={value} />
      <div className={styles.separator} />
      <div className={styles.dropdown}>
        <select className={styles.select} value={currency}>
          <option value={currency}>{currency}</option>
        </select>
        <div className={styles.buttonOverlay}>
          <CurrencyListButton />
        </div>
      </div>
    </div>
  );
};
