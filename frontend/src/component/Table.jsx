import React from 'react'

export const Table = ({ columns, children }) => {
    return (
        <div className="overflow-x-scroll rounded-box border border-base-content/5 bg-base-100 overflow-y-scroll mt-3 h-[65vh]">
            <table className="table">
                <thead>
                    <tr className='bg-gray-200 text-black text-center'>
                        {
                            columns.map((col, index) => {
                                return (
                                    <th key={index}>{col}</th>
                                )
                            })
                        }
                    </tr>
                </thead>
                <tbody>
                    {
                       children
                    }
                </tbody>
            </table>
        </div>
    )
}
