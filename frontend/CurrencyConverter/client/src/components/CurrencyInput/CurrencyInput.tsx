import styles from './CurrencyInput.module.scss'
import CurrencyListButton from './CurrencyListButton/CurrencyListButton'

type CurrencyInputProps = {
  value: string
  currency: string
}

function CurrencyInput({ value, currency }: CurrencyInputProps) {
  return (
    <div className={styles.container}>
      <input className={styles.input} type="text" value={value} readOnly />
      <div className={styles.separator} />
      <div className={styles.dropdown}>
        <select className={styles.select} value={currency}>
          <option value={currency}>{currency}</option>
        </select>
        <div className={styles['button-overlay']}>
          <CurrencyListButton />
        </div>
      </div>
    </div>
  )
}

export default CurrencyInput
