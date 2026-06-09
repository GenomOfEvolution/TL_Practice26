import { useState } from 'react';
import type { Currency } from '../../models';
import styles from './MoreAboutSection.module.scss';
import { MoreAboutButton } from './MoreAboutButton';
import { CurrencyInfo } from '../CurrencyInfo/CurrencyInfo';

type MoreAboutSectionProps = {
  from: Currency;
  to: Currency;
};

export const MoreAboutSection = ({ from, to }: MoreAboutSectionProps) => {
  const [isOpen, setIsOpen] = useState(false);

  const handleToggle = () => {
    setIsOpen((prev) => !prev);
  };

  return (
    <div className={styles.section}>
      <div>
        <div className={styles.buttonWrapper}>
          <MoreAboutButton
            fromValue={from.code}
            toValue={to.code}
            arrowUp={isOpen}
            onClick={handleToggle}
            testId="more-about-button"
          />
        </div>
        {isOpen && (
          <>
            <CurrencyInfo
              testId="first-info"
              title={`${from.name} - ${from.code} - ${from.symbol}`}
              description={from.description || 'No description available.'}
            />
            <CurrencyInfo
              testId="second-info"
              title={`${to.name} - ${to.code} - ${to.symbol}`}
              description={to.description || 'No description available.'}
            />
          </>
        )}
      </div>
    </div>
  );
};
