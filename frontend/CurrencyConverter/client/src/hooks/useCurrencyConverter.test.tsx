import { describe, expect, it } from 'vitest';
import { useCurrencyConverter } from './useCurrencyConverter';
import { act, renderHook } from '@testing-library/react';

describe('useCurrencyConverter', () => {
  it('auto-replaces to when setFrom matches current to', () => {
    const { result } = renderHook(() => useCurrencyConverter());

    act(() => {
      result.current.setTo('PLN');
    });

    expect(result.current.to.code).toBe('PLN');

    act(() => {
      result.current.setFrom('PLN');
    });

    expect(result.current.from.code).toBe('PLN');
    expect(result.current.to.code).not.toBe('PLN');
  });

  it('swap correctly recalculates result', () => {
    const { result } = renderHook(() => useCurrencyConverter());

    act(() => {
      result.current.setFrom('PLN');
      result.current.setTo('CAD');
      result.current.setAmount('1');
    });

    const oldResult = Number(result.current.result);

    act(() => {
      result.current.swap();
    });

    const newResult = Number(result.current.result);

    expect(oldResult * newResult).toBeCloseTo(1, 1);
  });
});
