import styles from './CurrencyInfo.module.scss'

type CurrencyInfoProps = {
  title: string
  description: string
}

function CurrencyInfo({ title, description }: CurrencyInfoProps) {
  return (
    <div className={styles.info}>
      <h2 className={styles.title}>{title}</h2>
      <p className={styles.description}>{description}</p>
    </div>
  )
}

export default CurrencyInfo
