import { useState } from 'react';
import type { Currency } from '../../models';
import styles from './MoreAboutSection.module.scss';
import { MoreAboutButton } from './MoreAboutButton';
import { CurrencyInfo } from '../CurrencyInfo/CurrencyInfo';

type MoreAboutSectionProps = {
  from: string;
  to: string;
  currencies: Currency[];
};

export const MoreAboutSection = ({ from, to, currencies }: MoreAboutSectionProps) => {
  const [isOpen, setIsOpen] = useState(false);

  const handleToggle = () => {
    setIsOpen((prev) => !prev);
  };

  const fromCurrency = currencies.find((c) => c.code === from);
  const toCurrency = currencies.find((c) => c.code === to);

  return (
    <div className={styles.section}>
      <div key={`${from}-${to}`}>
        <div className={styles.buttonWrapper}>
          <MoreAboutButton
            fromValue={from}
            toValue={to}
            arrowUp={isOpen}
            onClick={handleToggle}
            testId="more-about-button"
          />
        </div>
        {isOpen && fromCurrency && (
          <CurrencyInfo
            testId="first-info"
            title={`${fromCurrency.name} - ${fromCurrency.code} - ${fromCurrency.symbol}`}
            description={fromCurrency.description || 'No description available.'}
          />
        )}
        {isOpen && toCurrency && (
          <CurrencyInfo
            testId="second-info"
            title={`${toCurrency.name} - ${toCurrency.code} - ${toCurrency.symbol}`}
            description={toCurrency.description || 'No description available.'}
          />
        )}
      </div>
    </div>
  );
};
