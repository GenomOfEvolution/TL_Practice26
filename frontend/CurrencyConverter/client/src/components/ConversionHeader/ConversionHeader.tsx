import type { Currency } from '../../models';
import styles from './ConversionHeader.module.scss';

type ConversionHeaderProps = {
  from: Currency;
  to: Currency;
  amount: string;
  result: string;
  dateTime?: string;
};

const formatDate = (iso?: string): string | null => {
  if (!iso) return null;

  const date = new Date(iso);

  return date.toUTCString().replace('GMT', 'UTC');
};

export const ConversionHeader = ({ from, to, amount, result, dateTime }: ConversionHeaderProps) => {
  const dateLabel = formatDate(dateTime);

  return (
    <div className={styles.header}>
      <span className={styles.from}>
        {amount} {from.name} is
      </span>
      <span className={styles.to}>
        {result} {to.name}
      </span>
      {dateLabel && <span className={styles.date}>{dateLabel}</span>}
    </div>
  );
};
