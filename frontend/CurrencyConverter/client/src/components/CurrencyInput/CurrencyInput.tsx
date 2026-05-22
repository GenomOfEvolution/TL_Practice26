import styles from './CurrencyInput.module.scss'
import CurrencyListButton from '../buttons/CurrencyListButton/CurrencyListButton'

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
        <span className={styles.dropdownText}>{currency}</span>
        <CurrencyListButton />
      </div>
    </div>
  )
}

export default CurrencyInput
