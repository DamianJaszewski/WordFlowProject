

import { IconButton } from "../components";

function CustomTable ({ columns, data, actions }) {

    if (!data) {
        return (
            <p><em>Loading... Please refresh once the ASP.NET backend has started. See
                <a href="https://aka.ms/jspsintegrationreact">
                    https://aka.ms/jspsintegrationreact
                </a> for more details.
            </em></p>
        )
    } else {
        return (
            <table className="table table-striped" aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        {columns.map(col =>
                            <th>{col.header}</th>
                        )}
                        <th>Akcje</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map(myTask =>
                        <tr>
                            {columns.map(col =>
                                <td>{myTask[col.field]}</td>
                            )}
                            <td>
                                {actions.map(act =>
                                    <IconButton
                                        onClick={() => act.method(myTask)} // Dynamiczne przypisanie `myTask` do metody
                                        iconClass={act.iconName}
                                    />
                                )}
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        )
    }
}

export default CustomTable