import styles from './CurrencyConverter.module.scss'
import ConversionHeader from '../ConversionHeader/ConversionHeader'
import CurrencyInput from '../CurrencyInput/CurrencyInput'
import MoreAboutSection from '../MoreAboutSection/MoreAboutSection'

function CurrencyConverter() {
  return (
    <div className={styles.exchanger}>
      <ConversionHeader />
      <CurrencyInput value="1" currency="PLN" />
      <CurrencyInput value="0,99" currency="JPY" />
      <MoreAboutSection fromValue="PLN" toValue="JPY" />
    </div>
  )
}

export default CurrencyConverter
