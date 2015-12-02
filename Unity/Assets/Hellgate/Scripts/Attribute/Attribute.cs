﻿//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
//					Hellgate Framework
// Copyright © Uniqtem Co., Ltd.
//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
using UnityEngine;
using System;

namespace Hellgate
{
	public class AttributeMappingConfig<T>
	{
		public T t;
		public string name;
		public Type type;
	}

	public class TableAttribute : Attribute
	{
		private bool tableAutoGenerated;
		private string tableName;

		public bool TableAutoGenerated {
			get {
				return tableAutoGenerated;
			}
		}

		public string TableName {
			get {
				return tableName;
			}
		}

		public TableAttribute ()
		{
			this.tableName = "";
			tableAutoGenerated = false;
		}

		public TableAttribute (bool tableAutoGenerated)
		{
			tableName = "";
			this.tableAutoGenerated = tableAutoGenerated;
		}

		public TableAttribute (string tableName, bool tableAutoGenerated = false)
		{
			this.tableName = tableName;
			this.tableAutoGenerated = tableAutoGenerated;
		}
	}
	
	public class ColumnAttribute : Attribute
	{
		private SqliteDataConstraints[] constraints;
		private string type = "";
		private bool isConstraints = true;

		public ColumnAttribute ()
		{
			isConstraints = false;
		}

		public ColumnAttribute (SqliteDataConstraints constraints)
		{
			this.constraints = new SqliteDataConstraints[] { constraints };
		}

		public ColumnAttribute (SqliteDataConstraints[] constraints)
		{
			this.constraints = constraints;
		}

		public ColumnAttribute (string type)
		{
			isConstraints = false;
			this.type = type;
		}

		public ColumnAttribute (string type, SqliteDataConstraints constraints)
		{
			this.type = type;
			this.constraints = new SqliteDataConstraints[] { constraints };
		}

		public ColumnAttribute (string type, SqliteDataConstraints[] constraints)
		{
			this.type = type;
			this.constraints = constraints;
		}

		public string Type {
			get {
				return type;
			}
		}

		public SqliteDataConstraints[] Constraints {
			get {
				return constraints;
			}
		}

		/// <summary>
		/// Checks the constraints.
		/// </summary>
		/// <returns><c>true</c>, if constraints was checked, <c>false</c> otherwise.</returns>
		/// <param name="constraints">Constraints.</param>
		public bool CheckConstraints (SqliteDataConstraints constraints)
		{
			if (!isConstraints) {
				return false;
			}

			if (Array.FindIndex (this.constraints, c => c == constraints) < 0) {
				return false;
			} else {
				return true;
			}
		}
	}
	
	public class IgnoreAttribute : Attribute
	{
	}
}
