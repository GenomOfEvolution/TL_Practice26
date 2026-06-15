import { render, screen } from '@testing-library/react';
import { describe, expect, it } from 'vitest';
import { PriceChart } from './PriceChart';
import type { PriceChange } from '../../models';
import mockPriceHistory from '../../mocks/4_hw_mock_price_history.json';

const mockData = mockPriceHistory as PriceChange[];

describe('PriceChart', () => {
  it('shows loader when loading with no data', () => {
    render(<PriceChart data={[]} loading={true} />);

    expect(screen.getByTestId('price-chart-loader')).toBeInTheDocument();
  });

  it('shows error when error with no data', () => {
    render(<PriceChart data={[]} loading={false} error="Server error" />);

    expect(screen.getByTestId('price-chart-error')).toBeInTheDocument();
    expect(screen.getByText('Failed to load chart data.')).toBeInTheDocument();
    expect(screen.getByText('Server error')).toBeInTheDocument();
  });

  it('shows empty state when no data and not loading', () => {
    render(<PriceChart data={[]} loading={false} />);

    expect(screen.getByTestId('price-chart-empty')).toBeInTheDocument();
    expect(screen.getByText('No data for the selected period.')).toBeInTheDocument();
  });

  it('renders chart when data exists', () => {
    render(<PriceChart data={mockData} loading={false} />);

    expect(screen.getByTestId('price-chart')).toBeInTheDocument();
  });
});
