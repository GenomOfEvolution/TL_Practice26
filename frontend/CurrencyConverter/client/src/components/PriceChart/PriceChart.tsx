import { LineChart, Line, XAxis, YAxis, Tooltip, ResponsiveContainer, CartesianGrid } from 'recharts';
import type { PriceChange } from '../../models';
import styles from './PriceChart.module.scss';

type PriceChartProps = {
  data: PriceChange[];
  loading: boolean;
  error?: string;
};

type ChartPoint = {
  time: string;
  price: number;
};

const PRICE_DECIMALS = 6;

const formatTime = (iso: string): string => {
  const d = new Date(iso);

  return d.toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit', second: '2-digit', hour12: false });
};

const toChartData = (items: PriceChange[]): ChartPoint[] =>
  items.map((item) => ({ time: formatTime(item.dateTime), price: item.price }));

const CustomTooltip = ({ active, payload, label }: { active?: boolean; payload?: { value: number }[]; label?: string }) => {
  if (!active || !payload || !payload.length) return null;

  return (
    <div className={styles.tooltip}>
      <p className={styles['tooltip-time']}>{label}</p>
      <p className={styles['tooltip-price']}>{payload[0].value.toFixed(PRICE_DECIMALS)}</p>
    </div>
  );
};

export const PriceChart = ({ data, loading, error }: PriceChartProps) => {
  if (loading && data.length === 0) {
    return (
      <div className={styles['chart-box']}>
        <div className={styles['loading-state']}>
          <span className={styles.loader} data-testid="price-chart-loader" />
        </div>
      </div>
    );
  }

  if (error && data.length === 0) {
    return (
      <div className={styles['chart-box']}>
        <div className={styles.error}>
          <p>Failed to load chart data.</p>
          <p>{error}</p>
        </div>
      </div>
    );
  }

  if (data.length === 0) {
    return (
      <div className={styles['chart-box']}>
        <p className={styles.empty}>No data for the selected period.</p>
      </div>
    );
  }

  const chartData = toChartData(data);

  return (
    <div className={styles['chart-box']}>
      <ResponsiveContainer width="100%" height={300}>
        <LineChart data={chartData} margin={{ top: 8, right: 16, left: 8, bottom: 8 }}>
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="time" tick={{ fontSize: 12, fontFamily: "'Inter Variable', sans-serif" }} />
          <YAxis domain={['auto', 'auto']} tick={{ fontSize: 12, fontFamily: "'Inter Variable', sans-serif" }} />
          <Tooltip content={<CustomTooltip />} />
          <Line type="monotone" dataKey="price" stroke="#3b82f6" strokeWidth={2} dot={{ r: 3 }} />
        </LineChart>
      </ResponsiveContainer>
    </div>
  );
};
