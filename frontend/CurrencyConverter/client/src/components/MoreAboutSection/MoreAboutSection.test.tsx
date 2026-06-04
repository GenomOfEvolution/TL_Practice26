import { render, screen, fireEvent } from '@testing-library/react';
import { MoreAboutSection } from './MoreAboutSection';
import { currencies } from '../../mocks';

describe('MoreAboutSection', () => {
  it('renders the MoreAboutButton with testId', () => {
    render(<MoreAboutSection from="PLN" to="JPY" currencies={currencies} />);

    expect(screen.getByTestId('more-about-button')).toBeInTheDocument();
  });

  it('shows CurrencyInfo after clicking the button', () => {
    render(<MoreAboutSection from="PLN" to="JPY" currencies={currencies} />);

    fireEvent.click(screen.getByTestId('more-about-button'));

    expect(screen.getByTestId('first-info')).toBeInTheDocument();
    expect(screen.getByTestId('second-info')).toBeInTheDocument();
  });
});
