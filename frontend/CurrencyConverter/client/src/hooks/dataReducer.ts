export type DataState<T> = {
  data?: T;
  loading: boolean;
  error?: string;
};

export type DataAction<T> =
  | { type: 'LOADING' }
  | { type: 'SUCCESS'; payload: T }
  | { type: 'ERROR'; payload: string };

export const createInitialState = <T>(): DataState<T> => ({
  data: undefined,
  loading: false,
  error: undefined,
});

export const dataReducer = <T>(state: DataState<T>, action: DataAction<T>): DataState<T> => {
  switch (action.type) {
    case 'LOADING':
      return { ...state, loading: true, error: undefined };
    case 'SUCCESS':
      return { data: action.payload, loading: false, error: undefined };
    case 'ERROR':
      return { ...state, loading: false, error: action.payload };
    default:
      return state;
  }
};
