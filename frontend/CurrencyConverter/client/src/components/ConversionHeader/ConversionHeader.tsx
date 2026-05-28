import styles from './ConversionHeader.module.scss';

export const ConversionHeader = () => {
  return (
    <div className={styles.header}>
      <span className={styles.from}>1 Polish zloty is</span>
      <span className={styles.to}>0.99 Japanese yen</span>
      <span className={styles.date}>Fri, 05 Apr 2026 10:35 UTC</span>
    </div>
  );
};
