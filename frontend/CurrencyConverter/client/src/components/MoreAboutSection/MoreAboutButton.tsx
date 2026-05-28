import styles from './MoreAboutButton.module.scss';

type MoreAboutButtonProps = {
  fromValue: string
  toValue: string
  arrowUp?: boolean
}

function MoreAboutButton({ fromValue, toValue, arrowUp = true }: MoreAboutButtonProps) {
  return (
    <button className={styles.button}>{fromValue}/{toValue}: about {arrowUp ? '↑' : '↓'}</button>
  )
}

export default MoreAboutButton
