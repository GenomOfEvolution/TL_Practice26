import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { describe, expect, it, vi } from 'vitest';
import { PeriodSelector } from './PeriodSelector';

const DEFAULT_PERIOD = 5;
const CLICKED_PERIOD = 3;

describe('PeriodSelector', () => {
  it('renders all 5 period buttons', () => {
    render(<PeriodSelector period={DEFAULT_PERIOD} onPeriodChange={() => {}} />);

    expect(screen.getByText('1 MIN')).toBeInTheDocument();
    expect(screen.getByText('2 MIN')).toBeInTheDocument();
    expect(screen.getByText('3 MIN')).toBeInTheDocument();
    expect(screen.getByText('4 MIN')).toBeInTheDocument();
    expect(screen.getByText('5 MIN')).toBeInTheDocument();
  });

  it('calls onPeriodChange with correct period on click', async () => {
    const onPeriodChange = vi.fn();

    render(<PeriodSelector period={DEFAULT_PERIOD} onPeriodChange={onPeriodChange} />);

    await userEvent.click(screen.getByText('3 MIN'));

    expect(onPeriodChange).toHaveBeenCalledWith(CLICKED_PERIOD);
  });
});
