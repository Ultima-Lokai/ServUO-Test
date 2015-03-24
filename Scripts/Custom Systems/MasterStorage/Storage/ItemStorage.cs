﻿/*
 created by:
     /\            888                   888     .d8888b.   .d8888b.  
____/_ \____       888                   888    d88P  Y88b d88P  Y88b 
\  ___\ \  /       888                   888    888    888 888    888 
 \/ /  \/ /    .d88888  8888b.   8888b.  888888 Y88b. d888 Y88b. d888 
 / /\__/_/\   d88" 888     "88b     "88b 888     "Y888P888  "Y888P888 
/__\ \_____\  888  888 .d888888 .d888888 888           888        888 
    \  /      Y88b 888 888  888 888  888 Y88b.  Y88b  d88P Y88b  d88P 
     \/        "Y88888 "Y888888 "Y888888  "Y888  "Y8888P"   "Y8888P"  
*/
using System;
using Server;

namespace daat99
{
	public class ItemStorage : BaseStorage
	{
		public ItemStorage() : base() { }
		public ItemStorage(GenericReader reader) : base(reader) { }
		public override string Name { get { return "Everything"; } }
		private static Type[] defaultStoredTypes = new Type[] { typeof(Item) };
		public override Type[] DefaultStoredTypes { get { return ItemStorage.defaultStoredTypes; } }
		protected static new BaseStorage singleton; 
		public static new BaseStorage Storage { get { if (singleton == null) singleton = new ItemStorage(); return singleton; } }
		public override BaseStorage GetStorage() { return ItemStorage.Storage; }
	}
}
