import { render, screen, fireEvent } from '@testing-library/react';
import { CurrencyConverter } from '../components/CurrencyConverter/CurrencyConverter';

describe('CurrencyConverter', () => {
  it('renders inputs, selects, swap button and more-about button', () => {
    // Arrange
    render(<CurrencyConverter />);

    // Assert
    expect(screen.getByTestId('from-amount')).toBeInTheDocument();
    expect(screen.getByTestId('to-amount')).toBeInTheDocument();
    expect(screen.getByTestId('from-currency')).toBeInTheDocument();
    expect(screen.getByTestId('to-currency')).toBeInTheDocument();
    expect(screen.getByTestId('swap-button')).toBeInTheDocument();
    expect(screen.getByTestId('more-about-button')).toBeInTheDocument();
  });

  it('recalculates result when amount changes', () => {
    // Arrange
    render(<CurrencyConverter />);
    const fromAmount = screen.getByTestId<HTMLInputElement>('from-amount');
    const toAmount = screen.getByTestId<HTMLInputElement>('to-amount');

    // Act
    fireEvent.change(fromAmount, { target: { value: '2' } });

    // Assert
    expect(toAmount).toHaveValue('5.9');
  });

  it('recalculates result when currency pair changes', () => {
    // Arrange
    render(<CurrencyConverter />);
    const toAmount = screen.getByTestId<HTMLInputElement>('to-amount');
    const fromCurrency = screen.getByTestId<HTMLSelectElement>('from-currency');

    // Act
    fireEvent.change(fromCurrency, { target: { value: 'AUD' } });

    // Assert
    expect(toAmount).toHaveValue('2.66');
  });

  it('prevents selecting the same currency in both selects', () => {
    // Arrange
    render(<CurrencyConverter />);
    const fromCurrency = screen.getByTestId<HTMLSelectElement>('from-currency');
    const toCurrency = screen.getByTestId<HTMLSelectElement>('to-currency');

    // Act
    fireEvent.change(fromCurrency, { target: { value: 'PLN' } });

    // Assert
    expect(fromCurrency).toHaveValue('PLN');
    expect(toCurrency).not.toHaveValue('PLN');
  });

  it('resets MoreAboutSection isOpen when currency pair changes', () => {
    // Arrange
    render(<CurrencyConverter />);
    const moreAboutButton = screen.getByTestId('more-about-button');
    fireEvent.click(moreAboutButton);

    expect(screen.getByTestId('first-info')).toBeInTheDocument();
    expect(screen.getByTestId('second-info')).toBeInTheDocument();

    // Act
    const fromCurrency = screen.getByTestId<HTMLSelectElement>('from-currency');
    fireEvent.change(fromCurrency, { target: { value: 'AUD' } });

    // Assert
    expect(screen.queryByTestId('first-info')).not.toBeInTheDocument();
    expect(screen.queryByTestId('second-info')).not.toBeInTheDocument();
  });
});
