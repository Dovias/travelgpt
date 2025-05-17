import { render } from '@testing-library/react'
import App from '../../main/App'
import { describe, it } from 'vitest'

describe('App', () => {
    it('renders the App component', () => {
        render(<App />)
    })
})