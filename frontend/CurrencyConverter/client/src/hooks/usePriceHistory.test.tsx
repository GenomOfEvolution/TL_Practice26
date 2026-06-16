import { describe, expect, it, vi, beforeEach, afterEach } from 'vitest';
import { renderHook, waitFor } from '@testing-library/react';
import { usePriceHistory } from './usePriceHistory';
import type { PriceChangeDto } from '../models/dto';
import mockPriceHistory from '../mocks/4_hw_mock_price_history.json';

const { mockedFetchPriceChanges } = vi.hoisted(() => ({
  mockedFetchPriceChanges: vi.fn()
}));

vi.mock('../api/currencyApi', () => ({
  fetchPriceChanges: mockedFetchPriceChanges
}));

const from = { code: 'PLN', name: 'Polish zloty', description: '', symbol: 'zł' };
const to = { code: 'CAD', name: 'Canadian dollar', description: '', symbol: '$' };

const DEFAULT_PERIOD = 5;
const DATA_LENGTH = 2;
const POLL_INTERVAL_MS = 10_000;
const LONG_TIMEOUT_MS = 30_000;
const NEW_PERIOD = 3;
const CALLS_AFTER_INTERVAL = 2;
const CALLS_AFTER_TWO_INTERVALS = 3;

const mockDto = mockPriceHistory as PriceChangeDto[];

describe('usePriceHistory', () => {
  beforeEach(() => {
    mockedFetchPriceChanges.mockResolvedValue(mockDto);
  });

  afterEach(() => {
    vi.useRealTimers();
  });

  it('returns loading initially', () => {
    const { result } = renderHook(() => usePriceHistory(from, to, DEFAULT_PERIOD));

    expect(result.current.loading).toBe(true);
    expect(result.current.data).toBeUndefined();
  });

  it('returns data after successful fetch', async () => {
    const { result } = renderHook(() => usePriceHistory(from, to, DEFAULT_PERIOD));

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    expect(result.current.data).toHaveLength(DATA_LENGTH);
  });

  it('returns error when fetch fails', async () => {
    mockedFetchPriceChanges.mockRejectedValue(new Error('Network error'));

    const { result } = renderHook(() => usePriceHistory(from, to, DEFAULT_PERIOD));

    await waitFor(() => {
      expect(result.current.error).toBe('Network error');
    });

    expect(result.current.loading).toBe(false);
  });

  it('refetches on interval', async () => {
    vi.useFakeTimers();

    renderHook(() => usePriceHistory(from, to, DEFAULT_PERIOD));

    await vi.waitFor(() => {
      expect(mockedFetchPriceChanges).toHaveBeenCalledTimes(1);
    });

    vi.advanceTimersByTime(POLL_INTERVAL_MS);

    expect(mockedFetchPriceChanges).toHaveBeenCalledTimes(CALLS_AFTER_INTERVAL);

    vi.advanceTimersByTime(POLL_INTERVAL_MS);

    expect(mockedFetchPriceChanges).toHaveBeenCalledTimes(CALLS_AFTER_TWO_INTERVALS);
  });

  it('clears interval on unmount', async () => {
    vi.useFakeTimers();

    const { unmount } = renderHook(() => usePriceHistory(from, to, DEFAULT_PERIOD));

    await vi.waitFor(() => {
      expect(mockedFetchPriceChanges).toHaveBeenCalledTimes(1);
    });

    unmount();

    vi.advanceTimersByTime(LONG_TIMEOUT_MS);

    expect(mockedFetchPriceChanges).toHaveBeenCalledTimes(1);
  });

  it('refetches when period changes', async () => {
    const { result, rerender } = renderHook(({ from, to, period }) => usePriceHistory(from, to, period), {
      initialProps: { from, to, period: DEFAULT_PERIOD }
    });

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    mockedFetchPriceChanges.mockClear();

    rerender({ from, to, period: NEW_PERIOD });

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    expect(mockedFetchPriceChanges).toHaveBeenCalledTimes(1);
  });

  it('refetches when currency changes', async () => {
    const { result, rerender } = renderHook(({ from, to, period }) => usePriceHistory(from, to, period), {
      initialProps: { from, to, period: DEFAULT_PERIOD }
    });

    await waitFor(() => {
      expect(mockedFetchPriceChanges).toHaveBeenCalledTimes(1);
      expect(result.current.loading).toBe(false);
    });

    const newFrom = { ...from, code: 'AUD' };

    mockedFetchPriceChanges.mockClear();

    rerender({ from: newFrom, to, period: DEFAULT_PERIOD });

    await waitFor(() => {
      expect(mockedFetchPriceChanges).toHaveBeenCalledTimes(1);
    });
  });
});
