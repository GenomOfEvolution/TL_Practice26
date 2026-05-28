import styles from './CurrencyListButton.module.scss'

function CurrencyListButton() {
  return (
    <div className={styles['triangle-button']}>
      <div className={styles.triangle} />
    </div>
  )
}

export default CurrencyListButton
