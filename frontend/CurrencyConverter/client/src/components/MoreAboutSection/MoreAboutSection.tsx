import styles from './MoreAboutSection.module.scss';
import MoreAboutButton from './MoreAboutButton';
import CurrencyInfo from '../CurrencyInfo/CurrencyInfo';

const currencyData: Record<string, { title: string; description: string }> = {
  PLN: {
    title: 'Polish zloty - PLN - zł',
    description:
      'This is the official currency and legal tender of Poland. It is subdivided into 100 grosz-y (gr). It is the most traded currency in Central and Eastern Europe and ranks 21st most-traded in the foreign exchange market.'
  },
  JPY: {
    title: 'Japanese yen - JPY - ¥',
    description:
      'The yen is the official currency of Japan. It is the third-most traded currency in the foreign exchange market, after the United States dollar and the euro. It is also widely used as a third reserve currency after the US dollar and the euro.'
  }
};

type MoreAboutSectionProps = {
  fromValue: string;
  toValue: string;
};

function MoreAboutSection({ fromValue, toValue }: MoreAboutSectionProps) {
  const from = currencyData[fromValue];
  const to = currencyData[toValue];

  return (
    <div className={styles.section}>
      <div className={styles.buttonWrapper}>
        <MoreAboutButton fromValue={fromValue} toValue={toValue} />
      </div>
      {from && <CurrencyInfo testId="first-info" title={from.title} description={from.description} />}
      {to && <CurrencyInfo testId="second-info" title={to.title} description={to.description} />}
    </div>
  );
}

export default MoreAboutSection;
