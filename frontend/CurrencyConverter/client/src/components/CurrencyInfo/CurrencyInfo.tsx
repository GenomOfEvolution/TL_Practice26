import styles from './CurrencyInfo.module.scss'

type CurrencyInfoProps = {
  testId?: string
  title: string
  description: string
}

function CurrencyInfo({ testId, title, description }: CurrencyInfoProps) {
  return (
    <div data-testid={testId} className={styles.info}>
      <h2 className={styles.title}>{title}</h2>
      <p className={styles.description}>{description}</p>
    </div>
  )
}

export default CurrencyInfo
