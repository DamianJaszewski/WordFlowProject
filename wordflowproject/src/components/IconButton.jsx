

function IconButton({ onClick, iconClass }) {
    return (

        <button
            onClick={onClick}
            style={{
                background: 'none',
                border: 'none',
                cursor: 'pointer',
                padding: '0.1rem',
                paddingInline: '0.3rem',
                fontSize: '1.5rem',
                color: "rgb(21, 40, 55)",
            }}
        >
            <i className={`bi ${iconClass}`} />
        </button>
    )
}

export default IconButton