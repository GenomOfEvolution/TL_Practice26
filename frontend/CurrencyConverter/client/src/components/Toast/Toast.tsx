import { useEffect } from 'react';
import styles from './Toast.module.scss';

const AUTO_DISMISS_MS = 5000;

type ToastProps = {
  message: string;
  onClose: () => void;
};

export const Toast = ({ message, onClose }: ToastProps) => {
  useEffect(() => {
    const timer = setTimeout(onClose, AUTO_DISMISS_MS);

    return () => clearTimeout(timer);
  }, [onClose]);

  return (
    <div className={styles.toast} role="alert">
      <span className={styles.message}>{message}</span>
      <button className={styles.close} onClick={onClose} type="button" aria-label="Close">
        ×
      </button>
    </div>
  );
};
