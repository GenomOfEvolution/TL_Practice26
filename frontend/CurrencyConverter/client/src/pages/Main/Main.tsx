import styles from './Main.module.scss';
import { CurrencyConverter } from '../../components/CurrencyConverter/CurrencyConverter';

export const Main = () => {
  return (
    <div className={styles.main}>
      <CurrencyConverter />
    </div>
  );
};
