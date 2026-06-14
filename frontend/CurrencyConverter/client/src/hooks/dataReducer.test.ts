import { describe, expect, it } from 'vitest';
import { dataReducer, createInitialState } from './dataReducer';

describe('dataReducer', () => {
  it('sets loading on LOADING action', () => {
    const state = createInitialState<string[]>();
    const next = dataReducer(state, { type: 'LOADING' });

    expect(next.loading).toBe(true);
    expect(next.error).toBeUndefined();
  });

  it('stores data and clears loading on SUCCESS action', () => {
    const state = createInitialState<string[]>();
    const next = dataReducer(state, { type: 'SUCCESS', payload: ['a', 'b'] });

    expect(next.data).toEqual(['a', 'b']);
    expect(next.loading).toBe(false);
    expect(next.error).toBeUndefined();
  });

  it('stores error and clears loading on ERROR action', () => {
    const state = createInitialState<string[]>();
    const next = dataReducer(state, { type: 'ERROR', payload: 'Something went wrong' });

    expect(next.error).toBe('Something went wrong');
    expect(next.loading).toBe(false);
  });
});
