import { describe, expect, it } from 'vitest';
import { useCurrencyConverter } from './useCurrencyConverter';
import { act, renderHook } from '@testing-library/react';

describe('useCurrencyConverter', () => {
  it('same values replaces with different', () => {
    const { result } = renderHook(() => useCurrencyConverter());

    act(() => {
      result.current.setFrom('PLN');
      result.current.setTo('PLN');
    });

    expect(result.current.from.code !== result.current.to.code);
  });

  it('on value swap recalculates currency amount', () => {
    const { result } = renderHook(() => useCurrencyConverter());

    act(() => {
      result.current.setAmount('1');
    });

    const oldAmount = Number(result.current.amount);
    act(() => {
      result.current.swap();
    });

    const newAmount = Number(result.current.amount);
    expect(oldAmount).toEqual(1 / newAmount);
  });
});
