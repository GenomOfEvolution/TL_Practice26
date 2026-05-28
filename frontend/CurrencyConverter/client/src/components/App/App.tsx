import styles from './App.module.scss'
import CurrencyConverter from '../CurrencyConverter/CurrencyConverter'

function App() {
  return (
    <div className={styles.app}>
      <CurrencyConverter />
    </div>
  )
}

export default App
