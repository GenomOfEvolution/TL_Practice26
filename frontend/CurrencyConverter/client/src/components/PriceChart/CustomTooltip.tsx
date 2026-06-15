import styles from './PriceChart.module.scss';

const PRICE_DECIMALS = 6;

type CustomTooltipProps = {
  active?: boolean;
  payload?: { value: number }[];
  label?: string;
};

export const CustomTooltip = ({ active, payload, label }: CustomTooltipProps) => {
  if (!active || !payload || !payload.length) return null;

  return (
    <div className={styles.tooltip}>
      <p className={styles.tooltipTime}>{label}</p>
      <p className={styles.tooltipPrice}>{payload[0].value.toFixed(PRICE_DECIMALS)}</p>
    </div>
  );
};
