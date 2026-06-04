import styles from './App.module.scss';
import { CurrencyConverter } from '../CurrencyConverter/CurrencyConverter';

export const App = () => {
  return (
    <div className={styles.app}>
      <CurrencyConverter />
    </div>
  );
};
