import styles from './ConversionHeader.module.scss'

function ConversionHeader() {
  return (
    <div className={styles.header}>
      <span className={styles.fromLabel}>1 Polish zloty is</span>
      <span className={styles.toLabel}>0.99 Japanese yen</span>
      <span className={styles.dateLabel}>Fri, 05 Apr 2026 10:35 UTC</span>
    </div>
  )
}

export default ConversionHeader
