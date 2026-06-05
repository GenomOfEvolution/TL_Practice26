import { render, screen, fireEvent } from '@testing-library/react';
import { beforeEach, describe, expect, it, vi } from 'vitest';
import { CurrencyConverter } from '../components/CurrencyConverter/CurrencyConverter';
import { useCurrencyConverter } from '../hooks/useCurrencyConverter';
import { currencies } from '../mocks';

vi.mock('../hooks/useCurrencyConverter', () => ({
  useCurrencyConverter: vi.fn()
}));

const createUseCurrencyConverterMock = () => ({
  from: currencies[0],
  to: currencies[1],
  amount: '1',
  result: '5.9',
  currencies,
  priceChanges: {},
  setFrom: vi.fn(),
  setTo: vi.fn(),
  setAmount: vi.fn(),
  swap: vi.fn()
});

beforeEach(() => {
  vi.mocked(useCurrencyConverter).mockReturnValue(createUseCurrencyConverterMock());
});

describe('CurrencyConverter', () => {
  it('renders inputs, selects, swap button and more-about button', () => {
    render(<CurrencyConverter />);

    expect(screen.getByDisplayValue('1')).toBeInTheDocument();
    expect(screen.getByDisplayValue('5.9')).toBeInTheDocument();
    expect(screen.getByDisplayValue('CAD')).toBeInTheDocument();
    expect(screen.getByDisplayValue('PLN')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: /swap/i })).toBeInTheDocument();
    expect(screen.getByText(/CAD\/PLN.*about/)).toBeInTheDocument();
  });

  it('calls setAmount when amount input changes', () => {
    const setAmount = vi.fn();
    vi.mocked(useCurrencyConverter).mockReturnValue({ ...createUseCurrencyConverterMock(), setAmount });

    render(<CurrencyConverter />);
    const fromAmount = screen.getByDisplayValue('1');

    fireEvent.change(fromAmount, { target: { value: '2' } });

    expect(setAmount).toHaveBeenCalledWith('2');
  });

  it('calls setFrom when from currency changes', () => {
    const setFrom = vi.fn();
    vi.mocked(useCurrencyConverter).mockReturnValue({ ...createUseCurrencyConverterMock(), setFrom });

    render(<CurrencyConverter />);
    const fromCurrency = screen.getByDisplayValue('CAD');

    fireEvent.change(fromCurrency, { target: { value: 'AUD' } });

    expect(setFrom).toHaveBeenCalledWith('AUD');
  });

  it('calls setFrom with selected currency', () => {
    const setFrom = vi.fn();
    vi.mocked(useCurrencyConverter).mockReturnValue({ ...createUseCurrencyConverterMock(), setFrom });

    render(<CurrencyConverter />);
    const fromCurrency = screen.getByDisplayValue('CAD');

    fireEvent.change(fromCurrency, { target: { value: 'PLN' } });

    expect(setFrom).toHaveBeenCalledWith('PLN');
  });

  it('toggles MoreAboutSection and calls setFrom on pair change', () => {
    const setFrom = vi.fn();
    vi.mocked(useCurrencyConverter).mockReturnValue({ ...createUseCurrencyConverterMock(), setFrom });

    render(<CurrencyConverter />);
    const moreAboutButton = screen.getByText(/CAD\/PLN.*about/);

    fireEvent.click(moreAboutButton);
    expect(screen.getByText('Canadian dollar - CAD - $')).toBeInTheDocument();
    expect(screen.getByText('Polish zloty - PLN - zł')).toBeInTheDocument();

    fireEvent.click(moreAboutButton);
    expect(screen.queryByText('Polish zloty - PLN - zł')).not.toBeInTheDocument();

    const fromCurrency = screen.getByDisplayValue('CAD');
    fireEvent.change(fromCurrency, { target: { value: 'AUD' } });

    expect(setFrom).toHaveBeenCalledWith('AUD');
  });
});
