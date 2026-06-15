import { render, screen, fireEvent } from '@testing-library/react';
import { describe, expect, it, vi } from 'vitest';
import { PeriodSelector } from './PeriodSelector';

const DEFAULT_PERIOD = 5;
const CLICKED_PERIOD = 3;

describe('PeriodSelector', () => {
  it('renders all 5 period buttons', () => {
    render(<PeriodSelector period={DEFAULT_PERIOD} onPeriodChange={() => {}} />);

    expect(screen.getByTestId('period-1')).toBeInTheDocument();
    expect(screen.getByTestId('period-2')).toBeInTheDocument();
    expect(screen.getByTestId('period-3')).toBeInTheDocument();
    expect(screen.getByTestId('period-4')).toBeInTheDocument();
    expect(screen.getByTestId('period-5')).toBeInTheDocument();
  });

  it('calls onPeriodChange with correct period on click', () => {
    const onPeriodChange = vi.fn();

    render(<PeriodSelector period={DEFAULT_PERIOD} onPeriodChange={onPeriodChange} />);

    fireEvent.click(screen.getByTestId('period-3'));

    expect(onPeriodChange).toHaveBeenCalledWith(CLICKED_PERIOD);
  });
});
