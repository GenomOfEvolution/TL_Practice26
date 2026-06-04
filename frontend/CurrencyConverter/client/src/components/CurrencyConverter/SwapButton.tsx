import styles from './SwapButton.module.scss';

type SwapButtonProps = {
  onClick: () => void;
  testId?: string;
};

export const SwapButton = ({ onClick, testId }: SwapButtonProps) => {
  return (
    <button className={styles.button} onClick={onClick} type="button" data-testid={testId}>
      Swap ⇄
    </button>
  );
};
