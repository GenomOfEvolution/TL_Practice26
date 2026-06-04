import { render, screen } from '@testing-library/react';
import { MoreAboutSection } from './MoreAboutSection';

describe('MoreAboutSection', () => {
  it('renders the MoreAboutButton with testId', () => {
    render(<MoreAboutSection fromValue="PLN" toValue="JPY" />);

    expect(screen.getByTestId('more-about-button')).toBeInTheDocument();
  });

  it('renders CurrencyInfo components with testIds based on currency codes', () => {
    render(<MoreAboutSection fromValue="PLN" toValue="JPY" />);

    expect(screen.getByTestId('first-info')).toBeInTheDocument();
    expect(screen.getByTestId('second-info')).toBeInTheDocument();
  });
});
