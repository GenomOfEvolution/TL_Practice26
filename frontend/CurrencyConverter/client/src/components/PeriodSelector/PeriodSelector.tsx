import styles from './PeriodSelector.module.scss';

// eslint-disable-next-line @typescript-eslint/no-magic-numbers
const PERIODS = [1, 2, 3, 4, 5] as const;

type PeriodSelectorProps = {
  period: number;
  onPeriodChange: (period: number) => void;
};

export const PeriodSelector = ({ period, onPeriodChange }: PeriodSelectorProps) => {
  return (
    <div className={styles.periods} role="group" aria-label="Select period">
      {PERIODS.map((p) => (
        <button
          key={p}
          type="button"
          className={`${styles.button} ${period === p ? styles.active : ''}`}
          onClick={() => onPeriodChange(p)}
          data-testid={`period-${p}`}
        >
          {p} MIN
        </button>
      ))}
    </div>
  );
};
